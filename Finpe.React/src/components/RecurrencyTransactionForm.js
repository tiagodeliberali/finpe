import React from 'react';
import { Formik } from 'formik';
import { makeStyles } from '@material-ui/core/styles';

import TextField from '@material-ui/core/TextField';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import Button from '@material-ui/core/Button';
import DateFnsUtils from '@date-io/date-fns';
import {
  MuiPickersUtilsProvider,
  KeyboardDatePicker,
} from '@material-ui/pickers';
import Container from '@material-ui/core/Container';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Switch from '@material-ui/core/Switch';
import { postRecurrency } from '../utils/FinpeFetchData'
import { useAuth0 } from "./react-auth0-wrapper";

const useStyles = makeStyles({
  card: {
    width: 250,
    margin: 20,
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

export default function SimpleCard() {
  const classes = useStyles();
  const [hasEndDate, setHasEndDate] = React.useState(false);
  const { loading, getTokenSilently } = useAuth0();

  return (
    <div>
      <Formik
        initialValues={{ description: '', amount: '', date: new Date(), responsible: '', importance: 0, category: '' }}
        validate={values => {
          let errors = {};
          if (!values.amount) {
            errors.amount = 'Campo obrigatório';
          }
          return errors;
        }}
        onSubmit={(values, { setSubmitting }) => 
          getTokenSilently()
            .then(token => postRecurrency(token, values))
            .then(() => setSubmitting(false))
            .catch(error => {
              setSubmitting(false);
              alert(error);
            })
        }
      >
        {({
          values,
          errors,
          touched,
          handleChange,
          handleBlur,
          handleSubmit,
          isSubmitting,
          setFieldValue
        }) => (
            <form onSubmit={handleSubmit}>
              <Card className={classes.card}>
                <CardContent>
                  <Typography variant="h5" component="h2">
                    Conta recorrente
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
                    <TextField
                      id="amount"
                      label="Valor"
                      type="number"
                      onChange={handleChange}
                      onBlur={handleBlur}
                      value={values.amount}
                    />
                    {errors.amount && touched.amount && errors.amount}
                    <MuiPickersUtilsProvider utils={DateFnsUtils}>
                      <KeyboardDatePicker
                        disableToolbar
                        format="dd/MM/yyyy"
                        margin="normal"
                        id="date"
                        label="Data de início"
                        onChange={e => setFieldValue('date', e)}
                        onBlur={handleBlur}
                        value={values.date}
                        KeyboardButtonProps={{
                          'aria-label': 'change date',
                        }}
                      />
                    </MuiPickersUtilsProvider>
                    {errors.date && touched.date && errors.date}
                    <FormControlLabel
                      control={
                        <Switch
                          checked={hasEndDate}
                          onChange={event => setHasEndDate(event.target.checked)}
                          value="hasEndDate"
                          color="primary"
                        />
                      }
                      label="Tem data de fim"
                    />
                    {hasEndDate && <MuiPickersUtilsProvider utils={DateFnsUtils}>
                      <KeyboardDatePicker
                        disableToolbar
                        format="dd/MM/yyyy"
                        margin="normal"
                        id="endDate"
                        label="Data de fim"
                        onChange={e => setFieldValue('endDate', e)}
                        onBlur={handleBlur}
                        value={values.endDate}
                        KeyboardButtonProps={{
                          'aria-label': 'change date',
                        }}
                      />
                    </MuiPickersUtilsProvider>}
                    {hasEndDate && errors.endDate && touched.endDate && errors.endDate}
                    <TextField
                      id="responsible"
                      label="Responsável"
                      onChange={handleChange}
                      onBlur={handleBlur}
                      value={values.responsible}
                    />
                    {errors.responsible && touched.responsible && errors.responsible}
                    <FormControl>
                      <InputLabel htmlFor="importance">Importância</InputLabel>
                      <Select
                        id="importance"
                        onChange={handleChange}
                        onBlur={handleBlur}
                        value={values.importance}
                        inputProps={{
                          name: 'importance',
                          id: 'importance',
                        }}
                      >
                        <MenuItem value={0}>NotDefined</MenuItem>
                        <MenuItem value={1}>WeLikeIt</MenuItem>
                        <MenuItem value={2}>CanBeCut</MenuItem>
                        <MenuItem value={3}>HardToCut</MenuItem>
                        <MenuItem value={99}>Essential</MenuItem>
                      </Select>
                    </FormControl>
                    {errors.importance && touched.importance && errors.importance}
                    <TextField
                      id="category"
                      label="Categoria"
                      onChange={handleChange}
                      onBlur={handleBlur}
                      value={values.category}
                    />
                    {errors.category && touched.category && errors.category}

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