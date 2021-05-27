import { Link } from 'react-router-dom';

import classes from './NoAppsFound.module.css';

const NoAppsFound = () => {
  return (
    <div className={classes.noapps}>
      <p>No apps found!</p>
      <Link className='btn' to='/new-app'>
        Add a App
      </Link>
    </div>
  );
};

export default NoAppsFound;
