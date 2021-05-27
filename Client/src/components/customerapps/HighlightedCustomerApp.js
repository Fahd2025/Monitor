import classes from './HighlightedCustomerApp.module.css';

const HighlightedCustomerApp = (props) => {
 
  return (
    <figure className={classes.customerApp }>
      <p>{props.appInfo_Name}</p>
      <figcaption><strong> App Serial </strong>{props.appSerial}</figcaption>
      <figcaption><strong> App Version </strong>{props.appVersion}</figcaption>
      <figcaption><strong> Sys Info </strong>{props.sysInfo}</figcaption>
      <figcaption><strong> Install Date </strong>{props.installDate}</figcaption>
      <br /><br />
      <figcaption><strong> Customer </strong>{props.customer_Name}</figcaption>
      <figcaption><strong> VAT </strong>{props.customer_TaxNumber}</figcaption>
    </figure>
  );
};

export default HighlightedCustomerApp;
