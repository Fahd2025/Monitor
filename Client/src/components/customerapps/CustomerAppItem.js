import { Link } from 'react-router-dom';

import classes from './CustomerAppItem.module.css';

const CustomerAppItem = (props) => {
 
  return (
    <li className={classes.item}>
      <figure>
        <blockquote>
          <p>{props.appInfo_Name}</p>
        </blockquote>        
        <figcaption><strong> Serail: </strong> {props.appSerial}</figcaption>
      </figure>
      <Link className='btn' to={`/customerapps/${props.id}`}>
        View
      </Link>
    </li>
  );
};

export default CustomerAppItem;
