
import React, { Component } from "react";
import { hot } from "react-hot-loader";
import MenuBar from "./components/MenuBar"
import Chart from "./components/Chart"
import RecurrencyTransactionForm from "./components/RecurrencyTransactionForm"
import BudgetForm from "./components/BudgetForm"
import { Router, Link } from "@reach/router";

class App extends Component {
    render() {
        return (
            <div className="App">
                <MenuBar />
                <nav>
                    <Link to="/">Início</Link>{" "}
                    <Link to="/add-recurrency">Conta Recorrente</Link>{" "}
                    <Link to="/add-budget">Conta Recorrente</Link>{" "}
                </nav>

                <Router>
                    <Chart path="/" />
                    <RecurrencyTransactionForm path="/add-recurrency" />
                    <BudgetForm path="/add-budget" />
                </Router>
            </div>
        );
    }
}

export default hot(module)(App);