
import React, { Component } from "react";
import { hot } from "react-hot-loader";
import MenuBar from "./components/MenuBar"
import Chart from "./components/Chart"

class App extends Component {
    render() {
        return (
            <div className="App">
                <MenuBar />
                <Chart />
            </div>
        );
    }
}

export default hot(module)(App);