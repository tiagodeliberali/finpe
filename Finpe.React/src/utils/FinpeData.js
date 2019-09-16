const baseUrl = process.env.API_BASE_URL || 'https://localhost:44362/api/'

export const fetchApiData = () => 
    fetch(baseUrl + 'MonthlyView');

export const postRecurrency = (values) => 
    fetch(baseUrl + 'Recurrency', {
        method: 'POST',
        body: JSON.stringify(values),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })

export const postBudget = (values) => 
    fetch(baseUrl + 'Budget', {
        method: 'POST',
        body: JSON.stringify(values),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })