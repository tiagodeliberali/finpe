import React, { useState, useEffect } from 'react';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import useMediaQuery from '@material-ui/core/useMediaQuery';
import Grid from '@material-ui/core/Grid';
import { useAuth0 } from '../../utils/Auth0Wrapper';
import { fetchApiData } from '../../utils/FinpeFetchData';
import logError from '../../utils/Logger';

import OverviewBudgets from './OverviewBudgets';
import NextTransactions from './NextTransactions';


const useStyles = makeStyles({
  rootDesktop: {
    maxWidth: 1000,
    flexGrow: 1,
  },
  rootMobile: {
    maxWidth: 400,
    flexGrow: 1,
  },
  gridItem: {
    padding: 10,
  },
});

const loadData = (setState, token) => fetchApiData(token)
  .then((res) => res.json())
  .then((data) => {
    setState(data);
  })
  .catch(logError);

const HomeInfo = () => {
  const [token, setToken] = useState('');
  const [apiData, setApiData] = useState({ result: [] });
  const { loading, isAuthenticated, getTokenSilently } = useAuth0();
  const classes = useStyles();

  const theme = useTheme();
  const isDesktop = useMediaQuery(theme.breakpoints.up('sm'));

  useEffect(() => {
    async function fetchData() {
      if (loading || !isAuthenticated) {
        return;
      }

      const foundToken = await getTokenSilently();
      setToken(foundToken);
      await loadData(setApiData, foundToken);
    }
    fetchData();
  }, [loading, isAuthenticated, getTokenSilently]);

  const gridSize = isDesktop ? 6 : 12;

  return (
    <Grid container className={isDesktop ? classes.rootDesktop : classes.rootMobile} spacing={2}>
      <Grid xs={gridSize} className={classes.gridItem}>
        <OverviewBudgets data={apiData.result} />
      </Grid>
      <Grid xs={gridSize} className={classes.gridItem}>
        <NextTransactions data={apiData.result} token={token} />
      </Grid>
    </Grid>
  );
};


export default HomeInfo;
