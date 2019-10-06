import {
  deleteRecurrencyTransactionLine,
  deleteTransactionLine,
  consolidateTransactionLine,
  consolidateRecurrency,
} from './FinpeFetchData';

const buildAcumulatedData = (setState, data) => {
  const dataStatements = data.result
    .reduce((prev, x) => prev.concat(x.lines), [])
    .map((x) => Object.assign(x, {
      ticks: new Date(x.transactionDate).getTime(),
      longDate: x.transactionDate.substring(0, 10),
    }))
    .sort((a, b) => a - b)
    .reduce((prev, x) => {
      if (!prev[x.longDate]) {
        prev[x.longDate] = { // eslint-disable-line no-param-reassign
          amount: x.amount, items: [x],
        };
      } else {
        prev[x.longDate].amount += x.amount; // eslint-disable-line no-param-reassign
        prev[x.longDate].items.push(x);
      }
      return prev;
    }, {});

  let sumAmount = 0;
  const statements = [];
  const processedDates = Object.keys(dataStatements);
  const currentdate = new Date(processedDates[0]);
  while (currentdate <= new Date(processedDates[processedDates.length - 1])) {
    const longDate = currentdate.toISOString().substring(0, 10);

    let amount = 0;

    if (dataStatements[longDate]) {
      amount = dataStatements[longDate].amount;
      sumAmount += amount;
    }

    statements.push({
      amount, longDate, formatedDate: `${longDate.substring(8, 10)}/${longDate.substring(5, 7)}`, accumulatedAmount: sumAmount,
    });
    currentdate.setDate(currentdate.getDate() + 1);
  }

  setState({ data: statements, details: dataStatements });
};

export default buildAcumulatedData;

export const deleteTransaction = (token, item) => {
  if (item.id && item.id > 0) {
    deleteTransactionLine(token, item.id);
  }

  if (item.recurringTransactionId && item.recurringTransactionId > 0) {
    deleteRecurrencyTransactionLine(
      token, item.recurringTransactionId, item.date.year, item.date.month,
    );
  }
};

export const consolidateTransaction = (token, item) => {
  if (item.id && item.id > 0) {
    consolidateTransactionLine(token, item.id, item.amount);
  }

  if (item.recurringTransactionId && item.recurringTransactionId > 0) {
    consolidateRecurrency(
      token, item.recurringTransactionId, item.amount, item.date.year, item.date.month,
    );
  }
};
