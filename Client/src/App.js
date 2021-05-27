import React, { Suspense } from 'react';
import { Route, Switch, Redirect } from 'react-router-dom';

import Layout from './components/layout/Layout';
import LoadingSpinner from './components/UI/LoadingSpinner';

const NewQuote = React.lazy(() => import('./pages/NewQuote'));
const QuoteDetail = React.lazy(() => import('./pages/QuoteDetail'));
const NotFound = React.lazy(() => import('./pages/NotFound'));
const AllQuotes = React.lazy(() => import('./pages/AllQuotes'));

const AllApps = React.lazy(() => import('./pages/AllApps'));
const AppDetail = React.lazy(() => import('./pages/AppDetail'));

const AllCustomers = React.lazy(() => import('./pages/AllCustomers'));
const CustomerDetail = React.lazy(() => import('./pages/CustomerDetail'));

const AllCustomerApps = React.lazy(() => import('./pages/AllCustomerApps'));
const CustomerAppDetail = React.lazy(() => import('./pages/CustomerAppDetail'));

function App() {
  return (
    <Layout>
      <Suspense
        fallback={
          <div className='centered'>
            <LoadingSpinner />
          </div>
        }
      >
        <Switch>
          <Route path='/' exact>
            <Redirect to='/apps' />
          </Route>

          <Route path='/apps' exact>
            <AllApps />
          </Route>
          <Route path='/apps/:appId'>
            <AppDetail />
          </Route>

          <Route path='/customers' exact>
            <AllCustomers />
          </Route>
          <Route path='/customers/:customerId'>
            <CustomerDetail />
          </Route>

          <Route path='/customerapps' exact>
            <AllCustomerApps />
          </Route>
          <Route path='/customerapps/:customerAppId'>
            <CustomerAppDetail />
          </Route>

          <Route path='/quotes' exact>
            <AllQuotes />
          </Route>
          <Route path='/quotes/:quoteId'>
            <QuoteDetail />
          </Route>
          <Route path='/new-quote'>
            <NewQuote />
          </Route>
          
          <Route path='*'>
            <NotFound />
          </Route>
        </Switch>
      </Suspense>
    </Layout>
  );
}

export default App;
