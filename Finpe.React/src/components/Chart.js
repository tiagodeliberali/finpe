import React, { useState, useEffect  } from 'react';
import {
    ComposedChart, Bar, Brush, ReferenceLine, XAxis, YAxis, CartesianGrid, Tooltip, Legend, Area,
} from 'recharts';
import ChartTooltip from "./ChartTooltip"
import { fetchApiData } from "../utils/FinpeFetchData"
import { buildAcumulatedData } from "../utils/DataProcessor"
import { useAuth0 } from "./react-auth0-wrapper";

const CustomTooltip = ({ active, payload, details }) => {
    if (active) {
        return (
            payload && payload.length > 0 && details && <ChartTooltip resume={payload[0].payload} details={details[payload[0].payload.longDate]} />
        );
    }

    return null;
};

const loadData = (setState, token) => fetchApiData(token)
    .then(res => res.json())
    .then((data) => {
        buildAcumulatedData(setState, data)
    })
    .catch(console.log)

const Chart = () => {
    const [data, setData] = useState({});
    const { loading, isAuthenticated, getTokenSilently } = useAuth0();

    useEffect(() => {
        async function fetchData() {
            if (loading || !isAuthenticated) {
                return;
            }

            const token = await getTokenSilently();
            await loadData(setData, token)
        }
        fetchData();
      }, [loading, isAuthenticated, getTokenSilently]);

    return (
        <ComposedChart
            width={2000}
            height={500}
            data={data.data}
            margin={{
                top: 5, right: 30, left: 20, bottom: 5,
            }}
        >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="formatedDate" />
            <YAxis />
            <Tooltip content={<CustomTooltip details={data.details} />} />
            <Legend verticalAlign="top" wrapperStyle={{ lineHeight: '40px' }} />
            <ReferenceLine y={0} stroke="#000" />
            <Brush dataKey="formatedDate" height={30} stroke="#8884d8" />
            <Bar dataKey="amount" fill="#82ca9d" />
            <Area type="monotone" dataKey="accumulatedAmount" fill="#8884d8" stroke="#8884d8" />
        </ComposedChart>
    );
}


export default Chart;
