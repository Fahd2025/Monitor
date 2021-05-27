
import Wraper from '../animation/Wraper';
import classes from './HighlightedQuote.module.css';

const HighlightedQuote = (props) => {
 
  return (
    <Wraper> 
    <figure className={classes.quote }>
      <p>{props.text}</p>
      <figcaption>{props.author}</figcaption>
    </figure>
    </Wraper>
  );
};

export default HighlightedQuote;
