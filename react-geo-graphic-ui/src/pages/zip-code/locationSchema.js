import * as Yup from 'yup';

export const intialValues = {
    to: "",
    from: ""
};

export const validationSchema = Yup.object({
    from: Yup.number().required("From is required field").positive("From must be a positive number").integer("From must be a positive integer"),
    to: Yup.number().required("To is required field").positive("To must be a positive number").integer("To must be a positive integer"),
  });