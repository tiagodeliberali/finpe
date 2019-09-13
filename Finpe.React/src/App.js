
import React, { Component } from "react";
import { hot } from "react-hot-loader";
import MenuBar from "./components/MenuBar"
import Chart from "./components/Chart"
import RecurrencyTransactionForm from "./components/RecurrencyTransactionForm"
import { Router, Link } from "@reach/router";

class App extends Component {
    render() {
        return (
            <div className="App">
                <MenuBar />
                <nav>
                    <Link to="/add-recurrency">Home</Link>{" "}
                    <Link to="/">chart</Link>{" "}
                </nav>

                <Router>
                    <RecurrencyTransactionForm path="/add-recurrency" />
                    <Chart path="/" />
                </Router>
            </div>
        );
    }
}

export default hot(module)(App);