import { NavLink } from 'react-router-dom';

import classes from './MainNavigation.module.css';

const MainNavigation = () => {
  return (
    <header className={classes.header}>
      <div className={classes.logo}>Monitor</div>
      <nav className={classes.nav}>
        <ul>
          <li>
            <NavLink to='/apps' activeClassName={classes.active}>
              Apps
            </NavLink>
          </li>
          <li>
            <NavLink to='/customers' activeClassName={classes.active}>
              Customers
            </NavLink>
          </li>
          <li>
            <NavLink to='/quotes' activeClassName={classes.active}>
              All Quotes
            </NavLink>
          </li>
          <li>
            <NavLink to='/new-quote' activeClassName={classes.active}>
              Add a Quote
            </NavLink>
          </li>
        </ul>
      </nav>
    </header>
  );
};

export default MainNavigation;
