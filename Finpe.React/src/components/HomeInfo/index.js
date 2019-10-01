import React, { useState, useEffect  } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { useAuth0 } from "../../utils/Auth0Wrapper";
import { fetchApiData } from "../../utils/FinpeFetchData"

import OverviewBudgets from "./OverviewBudgets"
import NextTransactions from "./NextTransactions"

const useStyles = makeStyles({
    root: {
        maxWidth: 400
    },
  });

const loadData = (setState, token) => fetchApiData(token)
    .then(res => res.json())
    .then((data) => {
        setState(data)
    })
    .catch(console.log)

const HomeInfo = () => {
    const [apiData, setApiData] = useState([]);
    const { loading, isAuthenticated, getTokenSilently } = useAuth0();
    const classes = useStyles();

    useEffect(() => {
        async function fetchData() {
            if (loading || !isAuthenticated) {
                return;
            }

            const token = await getTokenSilently();
            await loadData(setApiData, token)
        }
        fetchData();
      }, [loading, isAuthenticated, getTokenSilently]);

    return (
        <div className={classes.root}>
            <OverviewBudgets data={apiData} />
            <NextTransactions data={apiData} />
        </div>
    );
}


export default HomeInfo;