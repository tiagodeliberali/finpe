
import React from "react";
import { hot } from "react-hot-loader";
import MenuBar from "./components/MenuBar"
import Chart from "./components/Chart"
import HomeInfo from "./components/HomeInfo"
import RecurrencyTransactionForm from "./components/RecurrencyTransactionForm"
import TransactionForm from "./components/TransactionForm"
import BudgetForm from "./components/BudgetForm"
import { Router } from "@reach/router";
import { makeStyles } from '@material-ui/core/styles';
import PrivateRoute from "./components/PrivateRoute"
import { useAuth0 } from "./components/react-auth0-wrapper";
import CssBaseline from '@material-ui/core/CssBaseline';

const useStyles = makeStyles(theme => ({
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
