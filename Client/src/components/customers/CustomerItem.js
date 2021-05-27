import { Link, useRouteMatch } from 'react-router-dom';

import classes from './CustomerItem.module.css';

const CustomerItem = (props) => {

  const match = useRouteMatch();

  return (
    <li className={classes.item}>
      <figure>
        <blockquote>
          <p>{props.name}</p>
        </blockquote>        
        <figcaption><strong> VAT: </strong> {props.taxNumber}</figcaption>
      </figure>
      <Link className='btn' to={`${match.url}/${props.id}`}>
        View
      </Link>
    </li>
  );
};

export default CustomerItem;
