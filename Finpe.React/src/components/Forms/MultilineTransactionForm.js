import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import List from '@material-ui/core/List';
import { useAuth0 } from '../../utils/Auth0Wrapper';
import { fetchMultilineData } from '../../utils/FinpeFetchData';
import logError from '../../utils/Logger';
import MultilineTransactionDialog from './MultilineTransactionDialog';


import TransactionItem from '../HomeInfo/TransactionItem';


const useStyles = makeStyles({
  rootList: {
    width: '100%',
  },
  rootGrid: {
    flexGrow: 1,
  },
  column: {
    flexBasis: '50%',
  },
  button: {
    marginBottom: 20,
  },
  bullet: {
    display: 'inline-block',
    margin: '0 2px',
    transform: 'scale(0.8)',
  },
  title: {
    fontSize: 18,
  },
  pos: {
    marginBottom: 12,
  },
  fab: {
    position: 'absolute',
    top: 10,
    right: 10,
  },
  fabDiv: {
    position: 'relative',
  },
});

const loadData = (setState, token) => fetchMultilineData(token)
  .then((res) => res.json())
  .then((data) => {
    setState(data.result);
  })
  .catch(logError);

const MultilineTransactionForm = () => {
  const [apiData, setApiData] = useState([]);
  const [open, setOpen] = React.useState(false);
  const [token, setToken] = React.useState('');
  const [parentId, setParentId] = React.useState(0);
  const { loading, isAuthenticated, getTokenSilently } = useAuth0();
  const classes = useStyles();

  const handleClickOpen = () => {
    setOpen(true);
    setParentId(0);
  };

  const handleMultilineClickOpen = (id) => {
    setOpen(true);
    setParentId(id);
  };

  const handleClose = () => {
    setOpen(false);
  };

  useEffect(() => {
    async function fetchData() {
      if (loading || !isAuthenticated) {
        return;
      }

      const foundToken = await getTokenSilently();
      setToken(foundToken);
      await loadData(setApiData, foundToken);
    }
    if (!open) {
      fetchData();
    }
  }, [loading, isAuthenticated, getTokenSilently, open]);

  const multilineDetails = apiData && apiData.map((item) => (
    <Grid item xs={12} key={JSON.stringify(item)}>
      <ExpansionPanel>
        <ExpansionPanelSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel1a-content"
          id="panel1a-header"
        >
          <div className={classes.column}>
            <Typography className={classes.title} color="textSecondary" gutterBottom>
              {item.description}
            </Typography>
            <Typography>
              R$
              {item.amount.toFixed(0)}
            </Typography>
          </div>
          <div className={classes.column}>
            <div className={classes.fabDiv}>
              <Fab size="small" color="primary" aria-label="add" className={classes.fab} onClick={() => handleMultilineClickOpen(item.id)}>
                <AddIcon />
              </Fab>
            </div>
          </div>
        </ExpansionPanelSummary>
        <ExpansionPanelDetails>
          <List className={classes.rootList}>
            {item.lines
              .map((row) => (
                <TransactionItem
                  key={JSON.stringify(row)}
                  item={row}
                  token={token}
                  allowConsolidate={false}
                />
              ))}
          </List>
        </ExpansionPanelDetails>
      </ExpansionPanel>
    </Grid>
  ));

  return (
    <>
      <Button variant="outlined" color="secondary" className={classes.button} onClick={handleClickOpen}>
        Nova fatura
      </Button>
      <MultilineTransactionDialog parentId={parentId} open={open} onClose={handleClose} />
      <Grid container className={classes.rootGrid} spacing={2}>
        {multilineDetails}
      </Grid>
    </>
  );
};


export default MultilineTransactionForm;
