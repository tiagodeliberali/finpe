import React, { useState, useEffect  } from 'react';
import { useAuth0 } from "./react-auth0-wrapper";
import { fetchApiData } from "../utils/FinpeFetchData"

import OverviewBudgets from "./OverviewBudgets"
import NextTransactions from "./NextTransactions"

const loadData = (setState, token) => fetchApiData(token)
    .then(res => res.json())
    .then((data) => {
        setState(data)
    })
    .catch(console.log)

const HomeInfo = () => {
    const [apiData, setApiData] = useState([]);
    const { loading, isAuthenticated, getTokenSilently } = useAuth0();

    useEffect(() => {
        async function fetchData() {
            if (loading || !isAuthenticated) {
                return;
            }

            const token = await getTokenSilently();
            await loadData(setApiData, token)
        }
        fetchData();
      }, [loading, isAuthenticated, getTokenSilently]);

    return (
        <div>
            <OverviewBudgets data={apiData} />
            <NextTransactions data={apiData} />
        </div>
    );
}


export default HomeInfo;