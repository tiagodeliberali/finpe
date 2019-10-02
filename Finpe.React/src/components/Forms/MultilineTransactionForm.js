import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';
import { useAuth0 } from '../../utils/Auth0Wrapper';
import { fetchMultilineData } from '../../utils/FinpeFetchData';
import MultilineTransactionDialog from './MultilineTransactionDialog';

const useStyles = makeStyles({
  card: {
  },
  rootGrid: {
    flexGrow: 1,
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
    bottom: 0,
    right: 0,
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
  .catch(console.log);

const MultilineTransactionForm = (props) => {
  const [apiData, setApiData] = useState([]);
  const [open, setOpen] = React.useState(false);
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

      const token = await getTokenSilently();
      await loadData(setApiData, token);
    }
    fetchData();
  }, [loading, isAuthenticated, getTokenSilently]);

  const multilineDetails = apiData && apiData.map((item) => (
    <Grid item xs={12} key={item.category}>
      <Card className={classes.card}>
        <CardContent>
          <Typography className={classes.title} color="textSecondary" gutterBottom>
            {item.description}
          </Typography>
          <Typography>
                    R$
            {' '}
            {item.amount.toFixed(0)}
          </Typography>
          <div className={classes.fabDiv}>
            <Fab size="small" color="primary" aria-label="add" className={classes.fab} onClick={() => handleMultilineClickOpen(item.id)}>
              <AddIcon />
            </Fab>
          </div>
        </CardContent>
      </Card>
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
