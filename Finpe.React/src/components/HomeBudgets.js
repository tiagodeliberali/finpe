import React, { useState, useEffect  } from 'react';
import Grid from '@material-ui/core/Grid';
import { fetchApiData } from "../utils/FinpeFetchData"
import { useAuth0 } from "./react-auth0-wrapper";
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
    return setState(data.result[0].budgets)
}

const loadData = (setState, token) => fetchApiData(token)
    .then(res => res.json())
    .then((data) => {
        buildBudgetData(setState, data)
    })
    .catch(console.log)

const HomeBudgets = () => {
    const [budgets, setBudgets] = useState([]);
    const { loading, isAuthenticated, getTokenSilently } = useAuth0();
    const classes = useStyles();

    useEffect(() => {
        async function fetchData() {
            if (loading || !isAuthenticated) {
                return;
            }

            const token = await getTokenSilently();
            await loadData(setBudgets, token)
        }
        fetchData();
      }, [loading, isAuthenticated, getTokenSilently]);

    const budgetDetails = budgets && budgets.map(item => (<Grid item xs={6} key={item.category}>
        <Card className={classes.card}>
            <CardContent>
            <Typography className={classes.title} color="textSecondary" gutterBottom>
                {item.category}
            </Typography>
            <Typography variant="h5" component="h2">
                R$ {item.available}
            </Typography>
            <Typography className={classes.pos} color="textSecondary">
                Gasto: R$ {item.used}
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


export default HomeBudgets;
