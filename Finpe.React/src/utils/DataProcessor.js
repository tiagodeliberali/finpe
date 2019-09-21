export const buildAcumulatedData = (setState, data) => {
    let startTime = new Date()
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

    setState({ data: statements, details: dataStatements })
    console.log("Dados processados em ", new Date() - startTime, 'ms')
}