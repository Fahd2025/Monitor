import { Link, useRouteMatch } from 'react-router-dom';

import classes from './AppItem.module.css';

const AppItem = (props) => {

  const match = useRouteMatch();

  return (
    <li className={classes.item}>
      <figure>
        <blockquote>
          <p>{props.name}</p>
        </blockquote>
        <figcaption>{props.description}</figcaption>
      </figure>
      <Link className='btn' to={`${match.url}/${props.id}`}>
        View
      </Link>
    </li>
  );
};

export default AppItem;
