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
import { postBudget } from '../utils/FinpeData'

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

export default function BudgetForm() {
    const classes = useStyles();

    return (
        <div>
            <Formik
                initialValues={{ day: 10, amount: '', category: '' }}
                validate={values => {
                    let errors = {};
                    if (!values.amount) {
                        errors.amount = 'Campo obrigatório';
                    }
                    return errors;
                }}
                onSubmit={(values, { setSubmitting }) => {
                    postBudget(values)
                        .then(() => {
                            setSubmitting(false);
                        }).catch(error => {
                            setSubmitting(false);
                            alert(error);
                        });
                }}
            >
                {({
                    values,
                    errors,
                    touched,
                    handleChange,
                    handleBlur,
                    handleSubmit,
                    isSubmitting,
                }) => (
                        <form onSubmit={handleSubmit}>
                            <Card className={classes.card}>
                                <CardContent>
                                    <Typography variant="h5" component="h2">
                                        Budget
                                    </Typography>
                                    <Container maxWidth="sm">
                                        <TextField
                                            id="category"
                                            label="Categoria"
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            value={values.category}
                                        />
                                        {errors.category && touched.category && errors.category}
                                        <TextField
                                            id="amount"
                                            label="Valor"
                                            type="number"
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            value={values.amount}
                                        />
                                        {errors.amount && touched.amount && errors.amount}
                                        <TextField
                                            id="day"
                                            label="Dia do mês"
                                            type="number"
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            value={values.day}
                                        />
                                        {errors.day && touched.day && errors.day}
                                    </Container>
                                </CardContent>
                                <CardActions>
                                    <Button variant="contained" color="primary" type="submit" disabled={isSubmitting}>
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