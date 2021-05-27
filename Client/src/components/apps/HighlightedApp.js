import classes from './HighlightedApp.module.css';

const HighlightedApp = (props) => {
 
  return (
    <figure className={classes.app }>
    <p>{props.name}</p>
    <figcaption>{props.description}</figcaption>
  </figure>
  );
};

export default HighlightedApp;
