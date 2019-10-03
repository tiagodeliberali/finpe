
import React from 'react';
import { hot } from 'react-hot-loader';
import { Router } from '@reach/router';
import { makeStyles } from '@material-ui/core/styles';
import CssBaseline from '@material-ui/core/CssBaseline';
import MenuBar from './components/MenuBar';
import Chart from './components/ChartInfo';
import HomeInfo from './components/HomeInfo/index';
import RecurrencyTransactionForm from './components/Forms/RecurrencyTransactionForm';
import TransactionForm from './components/Forms/TransactionForm';
import MultilineTransactionForm from './components/Forms/MultilineTransactionForm';
import BudgetForm from './components/Forms/BudgetForm';
import PrivateRoute from './utils/PrivateRoute';
import { useAuth0 } from './utils/Auth0Wrapper';

const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
  },
  toolbar: theme.mixins.toolbar,
  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
  },
}));

function App() {
  const { loading } = useAuth0();
  const classes = useStyles();

  if (loading) {
    return (
      <div>Loading...</div>
    );
  }
  return (
    <div className="App">
      <div className={classes.root}>
        <CssBaseline />
        <MenuBar />
        <main className={classes.content}>
          <div className={classes.toolbar} />
          <Router>
            <PrivateRoute path="/" component={HomeInfo} />
            <PrivateRoute path="/add-transaction" component={TransactionForm} />
            <PrivateRoute path="/add-multiline-transaction" component={MultilineTransactionForm} />
            <PrivateRoute path="/add-recurrency" component={RecurrencyTransactionForm} />
            <PrivateRoute path="/add-budget" component={BudgetForm} />
            <PrivateRoute path="/monthly-budgets" component={Chart} />
          </Router>
        </main>
      </div>
    </div>
  );
}

export default hot(module)(App);
