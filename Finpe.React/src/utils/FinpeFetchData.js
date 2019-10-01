const baseUrl = process.env.API_BASE_URL

const fetchWithToken = (token, destination, method, body) => {
    const config = {}
    
    var myHeaders = new Headers();
    myHeaders.append("Authorization", `Bearer ${token}`);
    
    if (method) config.method = method;
    if (body) {
        config.body = JSON.stringify(body)
        myHeaders.append("Content-type", "application/json; charset=UTF-8");
    }
    config.headers = myHeaders;

    return fetch(baseUrl + destination, config);
}

export const fetchApiData = (token) => fetchWithToken(token, 'MonthlyView');
export const fetchApiDataWithBudgets = (token, budgets) => fetchWithToken(token, 'MonthlyView', 'PUT', budgets);

export const postRecurrency = (token, values) => fetchWithToken(token, 'Recurrency', 'POST', values);

export const postTransaction = (token, values) => fetchWithToken(token, 'TransactionLine', 'POST', values);

export const fetchBudgets = (token) => fetchWithToken(token, 'Budget');
export const postBudget = (token, values) => fetchWithToken(token, 'Budget', 'POST', values);
export const putBudget = (token, values) => fetchWithToken(token, 'Budget', 'PUT', values);
