
import React, { Component } from "react";
import { hot } from "react-hot-loader";
import MenuBar from "./components/MenuBar"
import Chart from "./components/Chart"
import { Router, Link } from "@reach/router";

class App extends Component {
    render() {
        return (
            <div className="App">
                <MenuBar />
                <nav>
                    <Link to="/">Home</Link>{" "}
                    <Link to="chart">chart</Link>{" "}
                </nav>

                <Router>
                    <Chart path="/chart" />
                </Router>
            </div>
        );
    }
}

export default hot(module)(App);