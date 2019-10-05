import { captureException } from '@sentry/browser';

const logError = (error) => {
  captureException(error);
  console.log(error); // eslint-disable-line no-console
};

export default logError;
