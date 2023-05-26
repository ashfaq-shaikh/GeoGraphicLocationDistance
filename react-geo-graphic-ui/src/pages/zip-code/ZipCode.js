import React, { useState, useEffect } from "react";
import { Formik, Form } from "formik";
import { toast } from "react-hot-toast";
import { locationService } from "../../services/locationService";
import { validationSchema, intialValues } from "./locationSchema";

export const ZipCode = () => {
    const [locations, setLocations] = useState(null);
    const handleSubmit = (values) => {
        let loc = locationService({ from: values.from, to: values.to });
        loc.then(function(res){
            setLocations(res.data.data);
        })
        
        toast.success("Submited");
    };

    return (
        <>
            <Formik
                initialValues={intialValues}
                validationSchema={validationSchema}
                onSubmit={handleSubmit}
            >{({
                values,
                errors,
                touched,
                handleChange,
                handleBlur,
                handleSubmit
            }
            ) => (
                <>
                    <h2 className="text-center my-3">Enter Zip Code</h2>
                    <hr></hr>
                    <div className="container">
                        <Form>
                            <div className="row justify-content-center align-items-center">

                                <div className="col-md-3">
                                    <div className="form-group">
                                        <label className="fw-bold" htmlFor="from">From</label>
                                        <input type="number" value={values.from} onBlur={handleBlur} onChange={handleChange} className="form-control" name="from" placeholder="From" />
                                        {errors.from && touched.from ? <label className="text-danger"> {errors.from}   </label> : null}
                                    </div>
                                </div>
                                <div className="col-md-3">
                                    <div className="form-group">
                                        <label className="fw-bold" htmlFor="to">To</label>
                                        <input type="number" value={values.to} onBlur={handleBlur} onChange={handleChange} className="form-control" name="to" placeholder="To" />
                                        {errors.to && touched.to ? <label className="text-danger"> {errors.to} </label> : null}
                                    </div>
                                </div>
                            </div>
                            <div className="col-md-12 text-center mt-3">
                                <button type="submit" className="btn btn-primary btn-submit">Submit</button>
                            </div>
                            <div className="col-md-12 mt-3">
                                <div className="d-flex justify-content-center  align-items-center">
                                    <div className="result-container text-center  py-2">
                                        {locations ? <p className="fw-bold text-success"> The distance between  {locations?.from?.city} ({locations?.from?.zip}) to {locations?.to?.city} ({locations?.to?.zip}) is {locations?.distance.toFixed(2)} miles.</p> : null}
                                    </div>
                                </div>
                            </div>
                        </Form>
                    </div>
                </>
            )}
            </Formik>
        </>
    )
}