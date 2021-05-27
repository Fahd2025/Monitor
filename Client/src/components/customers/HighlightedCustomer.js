import classes from './HighlightedCustomer.module.css';

const HighlightedCustomer = (props) => {
 
  return (
    <figure className={classes.customer }>
      <p>{props.name}</p>
      <figcaption><strong> Phone </strong>{props.phone}</figcaption>
      <figcaption><strong> Address </strong>{props.address}</figcaption>
      <figcaption><strong> VAT </strong>{props.taxNumber}</figcaption>
    </figure>
  );
};

export default HighlightedCustomer;
