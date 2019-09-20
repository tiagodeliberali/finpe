const baseUrl = process.env.API_BASE_URL

export const fetchApiData = () => {
    const token = localStorage.getItem('jwt_token');
    var myHeaders = new Headers();
    myHeaders.append("Authorization", `Bearer ${token}`);
    return fetch(baseUrl + 'MonthlyView', { 
        headers: myHeaders
    });
}

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