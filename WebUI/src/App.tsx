import { Switch, Route } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import ProtectedRoute from './auth/protected-route';
import Home from './pages/Home';
import Profile from './pages/Profile';
import AddListing from './pages/AddListing';
import SingleItem from './pages/SingleItem';
import Loading from './components/loading';
import ItemDetails from './components/ItemDetails';
import Navbar from './components/Navbar';
import './styles/global.css';

const App = () => {
  const { isLoading } = useAuth0();

  if (isLoading) {
    return <Loading />;
  }

  return (
    <div id='app' className='d-flex flex-column h-100'>
      <Navbar />
      <Switch>
        <Route path='/' exact component={Home} />
        <Route path='/singleitem' exact component={SingleItem} />
        <Route path='/details/:id' exact component={ItemDetails} />
        <ProtectedRoute path='/addlisting' component={AddListing} />
        <ProtectedRoute path='/profile' component={Profile} />
      </Switch>
    </div>
  );
};

export default App;
