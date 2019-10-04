import React from 'react';
import { Formik } from 'formik';
import { makeStyles } from '@material-ui/core/styles';

import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';

import Container from '@material-ui/core/Container';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';

import PropTypes from 'prop-types';
import DatePicker from 'react-datepicker';
import ImportanceFormControl from './ImportanceFormControl';
import CategoryFormControl from './CategoryFormControl';
import { postTransaction } from '../../utils/FinpeFetchData';
import { useAuth0 } from '../../utils/Auth0Wrapper';
import logError from '../../utils/Logger';

import "react-datepicker/dist/react-datepicker.css";

const useStyles = makeStyles({
  card: {
    width: 250,
    margin: 20,
  },
  datepicker: {
    marginTop: 20,
  },
  bullet: {
    display: 'inline-block',
    margin: '0 2px',
    transform: 'scale(0.8)',
  },
  formControl: {
    margin: 5,
    minWidth: 120,
  },
  title: {
    fontSize: 14,
  },
  pos: {
    marginBottom: 12,
  },
});

export default function TransactionForm(props) {
  const classes = useStyles();
  const { loading, getTokenSilently } = useAuth0();
  let { isMultiline, parentId, onSuccess } = props;

  isMultiline = isMultiline || false;
  parentId = parentId || 0;
  const isMultilineTransaction = isMultiline && parentId === 0;

  return (
    <div>
      <Formik
        initialValues={{
          description: '', amount: 0, date: new Date(), responsible: '', importance: 0, category: '', isMultiline, multilineParentId: parentId,
        }}
        validate={() => {
          const errors = {};
          return errors;
        }}
        onSubmit={(values, { setSubmitting }) => getTokenSilently()
          .then((token) => postTransaction(token, values))
          .then(() => {
            setSubmitting(false);
            onSuccess();
          })
          .catch((error) => {
            setSubmitting(false);
            logError(error);
          })}
      >
        {({
          values,
          errors,
          touched,
          handleChange,
          handleBlur,
          handleSubmit,
          isSubmitting,
          setFieldValue,
        }) => (
          <form onSubmit={handleSubmit}>
            <Card className={classes.card}>
              <CardContent>
                <Typography variant="h5" component="h2">
                    Despesa
                </Typography>
                <Container maxWidth="sm">
                  <TextField
                    id="description"
                    label="Descrição"
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.description}
                  />
                  {errors.description && touched.description && errors.description}
                  {!isMultilineTransaction && (
                    <>
                      <TextField
                        id="amount"
                        label="Valor"
                        type="number"
                        onChange={handleChange}
                        onBlur={handleBlur}
                        value={values.amount}
                      />
                        {errors.amount && touched.amount && errors.amount}
                    </>
                  )}
                  <DatePicker
                    className={classes.datepicker}
                    id="date"
                    selected={values.date}
                    onChange={(date) => setFieldValue('date', date)}
                    onBlur={handleBlur}
                  />
                  {errors.date && touched.date && errors.date}
                  {!isMultilineTransaction && (
                    <>
                      <TextField
                        id="responsible"
                        label="Responsável"
                        onChange={handleChange}
                        onBlur={handleBlur}
                        value={values.responsible}
                      />
                        {errors.responsible && touched.responsible && errors.responsible}
                      <ImportanceFormControl
                        handleChange={handleChange}
                        handleBlur={handleBlur}
                        value={values.importance}
                      />
                        {errors.importance && touched.importance && errors.importance}
                      <CategoryFormControl
                        handleChange={handleChange}
                        handleBlur={handleBlur}
                        value={values.category}
                      />
                        {errors.category && touched.category && errors.category}
                    </>
                  )}
                </Container>
              </CardContent>
              <CardActions>
                <Button variant="contained" color="primary" type="submit" disabled={isSubmitting || loading}>
                    Enviar
                </Button>
              </CardActions>
            </Card>
          </form>
        )}
      </Formik>
    </div>
  );
}

TransactionForm.propTypes = {
  isMultiline: PropTypes.bool,
  parentId: PropTypes.number,
  onSuccess: PropTypes.func,
};

TransactionForm.defaultProps = {
  isMultiline: false,
  parentId: 0,
  onSuccess: () => {},
};
