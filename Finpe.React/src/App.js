
import React from "react";
import { hot } from "react-hot-loader";
import MenuBar from "./components/MenuBar"
import Chart from "./components/Chart"
import RecurrencyTransactionForm from "./components/RecurrencyTransactionForm"
import BudgetForm from "./components/BudgetForm"
import { Router, Link } from "@reach/router";
import PrivateRoute from "./components/PrivateRoute";
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
                <Link to="/add-recurrency">Conta Recorrente</Link>{" "}
                <Link to="/add-budget">Conta Recorrente</Link>{" "}
            </nav>)}

            <Router>
                {/* <PrivateRoute path="/" component={Chart} /> */}
                <Chart path="/chart" />
                <RecurrencyTransactionForm path="/add-recurrency" />
                <BudgetForm path="/add-budget" />
            </Router>
        </div>
    );
}

export default hot(module)(App);
