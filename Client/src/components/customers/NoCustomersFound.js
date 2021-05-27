import { Link } from 'react-router-dom';

import classes from './NoCustomersFound.module.css';

const NoCustomersFound = () => {
  return (
    <div className={classes.nocustomers}>
      <p>No customers found!</p>
      <Link className='btn' to='/new-customer'>
        Add a Customer
      </Link>
    </div>
  );
};

export default NoCustomersFound;
