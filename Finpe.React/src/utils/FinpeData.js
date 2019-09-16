const baseUrl = process.env.API_BASE_URL || 'https://finpe-api-forno.azurewebsites.net/api/'

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