import React, { useState, useEffect } from 'react';
import { withStyles, makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Slider from '@material-ui/core/Slider';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import { fetchBudgets, fetchApiDataWithBudgets, putBudget } from '../../utils/FinpeFetchData';
import { useAuth0 } from '../../utils/Auth0Wrapper';
import logError from '../../utils/Logger';


const useStyles = makeStyles((theme) => ({
  root: {
    width: 1370 + theme.spacing(3) * 2,
    padding: theme.spacing(3),
    margin: 50,
  },
  rootGrid: {
    flexGrow: 1,
  },
  margin: {
    height: theme.spacing(3),
  },
  button: {
    margin: theme.spacing(1),
  },
}));

const PrettoSlider = withStyles({
  root: {
    color: '#52af77',
    height: 8,
  },
  thumb: {
    height: 24,
    width: 24,
    backgroundColor: '#fff',
    border: '2px solid currentColor',
    marginTop: -8,
    marginLeft: -12,
    '&:focus,&:hover,&$active': {
      boxShadow: 'inherit',
    },
  },
  active: {},
  valueLabel: {
    left: 'calc(-50% + 4px)',
  },
  track: {
    height: 8,
    borderRadius: 4,
  },
  rail: {
    height: 8,
    borderRadius: 4,
  },
})(Slider);

const ChartBudgets = (props) => {
  const classes = useStyles();
  const [submitting, setSubmitting] = useState(false);
  const [simulated, setSimulated] = useState(false);
  const [totalBudget, setTotalBudget] = useState(0);
  const [budgetOriginalData, setBudgetOriginalData] = useState([]);
  const [budgetEditedData, setBudgetEditedData] = React.useState({});
  const { loading, isAuthenticated, getTokenSilently } = useAuth0();
  const { updateChartData } = props;

  const loadData = (token) => fetchBudgets(token)
    .then((res) => res.json())
    .then((res) => setBudgetOriginalData(res.result))
    .catch((e) => logError(e));

  const parseBudgetEditedData = () => Object.getOwnPropertyNames(budgetEditedData)
    .map((category) => ({ category, ...budgetEditedData[category] }));

  const buildBudgetData = () => {
    const budgets = parseBudgetEditedData();

    const result = budgetOriginalData && budgetOriginalData.map((item) => {
      const newBudget = budgets.filter((x) => x.category === item.category);
      return newBudget.length > 0
        ? newBudget[0]
        : { category: item.category, day: item.executionDay, amount: item.available };
    });

    return result;
  };

  const calculateTotalBudget = () => {
    const allBudgets = buildBudgetData();
    const totalBudgetAmount = !allBudgets
      ? 0
      : allBudgets.reduce((total, nextItem) => total + nextItem.amount, 0);
    setTotalBudget(totalBudgetAmount);
  };

  const simulate = () => {
    setSubmitting(true);
    setSimulated(true);

    return getTokenSilently()
      .then((token) => fetchApiDataWithBudgets(token, buildBudgetData()))
      .then((res) => res.json())
      .then((res) => {
        updateChartData(res);
        setSubmitting(false);
      })
      .catch((error) => {
        setSubmitting(false);
        logError(error);
      });
  };

  const saveBudgets = () => {
    const budgets = parseBudgetEditedData();

    if (!budgets || budgets.length === 0) {
      return;
    }

    setSubmitting(true);
    getTokenSilently()
      .then((token) => budgets.forEach((item) => putBudget(token, item)))
      .then(() => setSubmitting(false))
      .catch((error) => {
        setSubmitting(false);
        logError(error);
      });
  };

  const handleInputChange = (componentName, amount, day) => {
    if (typeof amount === 'number') {
      setSimulated(false);
      setBudgetEditedData(Object.assign(budgetEditedData, { [componentName]: { amount, day } }));
      calculateTotalBudget();
    }
  };

  useEffect(() => {
    async function fetchData() {
      if (loading || !isAuthenticated) {
        return;
      }

      const token = await getTokenSilently();
      await loadData(token);
    }
    fetchData();
  }, [loading, isAuthenticated, getTokenSilently]);

  useEffect(() => {
    calculateTotalBudget();
  }, [budgetOriginalData]);

  const budgets = budgetOriginalData && budgetOriginalData.map((item) => (
    <Grid item xs={4} key={item.category}>
      <Typography gutterBottom>{item.category}</Typography>
      <PrettoSlider valueLabelDisplay="auto" aria-label="pretto slider" defaultValue={item.available} max={5000} step={100} onChange={(event, newValue) => handleInputChange(item.category, newValue, item.executionDay)} />
    </Grid>
  ));

  return (
    <Paper className={classes.root}>
      <Grid container className={classes.rootGrid} spacing={2}>
        {budgets}
      </Grid>
      <Grid container className={classes.rootGrid} spacing={2} alignItems="center">
        <Grid item xs={1}>
          <Button variant="contained" color="primary" className={classes.button} disabled={submitting} onClick={simulate}>
                        Simular
          </Button>
        </Grid>
        <Grid item xs={1}>
          <Button variant="contained" color="secondary" className={classes.button} disabled={submitting || !simulated} onClick={saveBudgets}>
                        Salvar
          </Button>
        </Grid>
        <Grid item xs={2}>
          <Typography>
                        Total de budgets: R$
            {totalBudget}
          </Typography>
        </Grid>
      </Grid>
    </Paper>
  );
};

ChartBudgets.propTypes = {
  updateChartData: PropTypes.func.isRequired,
};

export default ChartBudgets;
