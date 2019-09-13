import React, { PureComponent } from 'react';
import {
    ComposedChart, Bar, Brush, ReferenceLine, XAxis, YAxis, CartesianGrid, Tooltip, Legend, Line, Area,
} from 'recharts';
import ChartTooltip from "./ChartTooltip"

const CustomTooltip = ({ active, payload, label, details }) => {
    if (active) {
        return (
            <ChartTooltip resume={payload[0].payload} details={details[payload[0].payload.longDate]} />
        );
    }

    return null;
};

class Chart extends PureComponent {
    constructor(props) {
        super(props);
        this.state = { data: [] };
    }

    componentDidMount() {
        console.log("buscando dados...")
        let startTime = new Date()

        fetch('https://localhost:44362/api/MonthlyView')
            .then(res => res.json())
            .then((data) => {
                console.log("Dados recebidos em ", new Date() - startTime, 'ms')
                startTime = new Date()
                const dataStatements = data.result
                    .reduce((prev, x) => prev.concat(x.lines), [])
                    .map(x => Object.assign(x, { ticks: new Date(x.transactionDate).getTime(), longDate: x.transactionDate.substring(0, 10) }))
                    .sort((a, b) => a - b)
                    .reduce((prev, x) => {
                        if (!prev[x.longDate]) {
                            prev[x.longDate] = { amount: x.amount, items: [x] }
                        } else {
                            prev[x.longDate].amount += x.amount
                            prev[x.longDate].items.push(x)
                        }
                        return prev
                    }, {});

                let sumAmount = 0;
                const statements = []
                const processedDates = Object.keys(dataStatements)
                let currentdate = new Date(processedDates[0])
                while (currentdate <= new Date(processedDates[processedDates.length - 1])) {
                    const longDate = currentdate.toISOString().substring(0, 10);

                    let amount = 0

                    if (!!dataStatements[longDate]) {
                        amount = dataStatements[longDate].amount;
                        sumAmount += amount;

                    }

                    statements.push({ amount, longDate, formatedDate: longDate.substring(8, 10) + '/' + longDate.substring(5, 7), accumulatedAmount: sumAmount });
                    currentdate.setDate(currentdate.getDate() + 1)
                }

                this.setState({ data: statements, details: dataStatements })
                console.log(this.state.data)
                console.log("Dados processados em ", new Date() - startTime, 'ms')
            })
            .catch(console.log)
    }

    render() {
        return (
            <ComposedChart
                width={2000}
                height={500}
                data={this.state.data}
                margin={{
                    top: 5, right: 30, left: 20, bottom: 5,
                }}
            >
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="formatedDate" />
                <YAxis />
                <Tooltip content={<CustomTooltip details={this.state.details} />} />
                <Legend verticalAlign="top" wrapperStyle={{ lineHeight: '40px' }} />
                <ReferenceLine y={0} stroke="#000" />
                <Brush dataKey="formatedDate" height={30} stroke="#8884d8" />
                <Bar dataKey="amount" fill="#82ca9d" />
                <Area type="monotone" dataKey="accumulatedAmount" fill="#8884d8" stroke="#8884d8" />
            </ComposedChart>
        );
    }
}

export default Chart;


