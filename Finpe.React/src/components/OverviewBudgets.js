import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';

const useStyles = makeStyles({
  card: {
  },
  rootGrid: {
    flexGrow: 1,
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
});

const buildBudgetData = (setState, data) => {
  return setState(data.budgets)
}

const OverviewBudgets = (props) => {
  const [budgets, setBudgets] = useState([]);
  const classes = useStyles();
  const { data } = props

  useEffect(() => {
    data && data.result && data.result[0] && buildBudgetData(setBudgets, data.result[0])
  }, [data]);

  const budgetDetails = budgets && budgets.map(item => (<Grid item xs={6} key={item.category}>
    <Card className={classes.card}>
      <CardContent>
        <Typography className={classes.title} color="textSecondary" gutterBottom>
          {item.category}
        </Typography>
        <Typography variant="h5" component="h2">
          R$ {item.available.toFixed(0)}
        </Typography>
        <Typography className={classes.pos} color="textSecondary">
          Gasto: R$ {item.used.toFixed(0)}
        </Typography>
      </CardContent>
    </Card>
  </Grid>))

  return (
    <Grid container className={classes.rootGrid} spacing={2}>
      {budgetDetails}
    </Grid>
  );
}


export default OverviewBudgets;
