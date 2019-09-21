
import React from "react";
import { hot } from "react-hot-loader";
import MenuBar from "./components/MenuBar"
import Chart from "./components/Chart"
import RecurrencyTransactionForm from "./components/RecurrencyTransactionForm"
import BudgetForm from "./components/BudgetForm"
import { Router, Link } from "@reach/router";
import PrivateRoute from "./components/PrivateRoute"
import { useAuth0 } from "./components/react-auth0-wrapper";

function App() {
    const { loading, isAuthenticated } = useAuth0();

    if (loading) {
        return (
            <div>Loading...</div>
        );
    }
    return (
        <div className="App">
            <MenuBar />
            {isAuthenticated && (
            <nav>
                <Link to="/">Início</Link>{" "}
                <Link to="/chart">Acumulado</Link>{" "}
                <Link to="/add-recurrency">Conta Recorrente</Link>{" "}
                <Link to="/add-budget">Budget</Link>{" "}
            </nav>)}

            {!isAuthenticated && (
            <nav>
                <Link to="/add-recurrency">Conta Recorrente</Link>{" "}
            </nav>)}

            <Router>
                <PrivateRoute path="/chart" component={Chart} />
                <PrivateRoute path="/add-recurrency" component={RecurrencyTransactionForm} />
                <PrivateRoute path="/add-budget" component={BudgetForm} />
            </Router>
        </div>
    );
}

export default hot(module)(App);
