import { useAuth0 } from '@auth0/auth0-react';
import { STATUS_CODES } from 'http';
import { Container, Navbar, Nav, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import logo from '../logo.svg';
import AuthenticationButton from './authentication-button';

const StyledNavbar = styled(Navbar)`
  border: 0;
  padding-top: 16px;
  padding-bottom: 16px;
  box-shadow: 0 1px 4px rgba(64, 87, 109, 0.07);
`;

const StyledImage = styled.img`
  border-radius: 50%;
  max-height: 50px;
  max-width: 50px;
  border: 5px solid rgba(0, 0, 0, 0.04);
  display: inline-block;
    vertical-align: middle;
    transform: translateZ(0);
    box-shadow: 0 0 1px rgba(0, 0, 0, 0);
    backface-visibility: hidden;
    -moz-osx-font-smoothing: grayscale;
    transition-duration: 0.3s;
    transition-property: transform;
    margin-right: 1rem;
  &:hover {
    transform: scale(1.1);
  }
`;

const StyledToggle = styled(Navbar.Toggle)`
  border: 0;

  &:focus {
    outline: none;
    box-shadow: none;
    background-color: rgba(0, 0, 0, 0.04);
    border-radius: 4px;
  }
`;

const StyledCollapse = styled(Navbar.Collapse)`
  @media (max-width: 992px) {
    margin-top: 8px;
    display: flex;
    flex-flow: column;

    .navbar-nav {
      width: 100%;
      margin-top: 4px;
    }

    #nav-left {
      order: 2;
    }

    #nav-right {
      order: 1;
    }
  }
`;

const StyledNavLink = styled(Nav.Link)`
  padding: 0 8px;
  margin-right: 8px;

  &:hover {
    background-color: rgba(0, 0, 0, 0.04);
    border-radius: 4px;
  }

  @media (max-width: 992px) {
    margin: 4px 0;
    padding: 0 16px !important;
  }
`;

const NavigationBar = () => {
  const { user } = useAuth0();
  const { isAuthenticated } = useAuth0();
  // const { picture } = user;
  return (
    <StyledNavbar expand='lg' variant='light'>
      <Container>
        <StyledNavbar.Brand href='#home'>
          <img src={logo} alt='logo' />
        </StyledNavbar.Brand>
        <StyledToggle aria-controls='responsive-navbar-nav' />
        <StyledCollapse id='responsive-navbar-nav'>
          <Nav id='nav-left' className='me-auto'>
            <StyledNavLink>
              <Link to='/' className='nav-link'>
                Home
              </Link>
            </StyledNavLink>
            {isAuthenticated && (
              <StyledNavLink>
                <Link to='/profile' className='nav-link'>
                  Profile
                </Link>
              </StyledNavLink>
            )}
            {isAuthenticated && (
              <StyledNavLink>
                <Link to='/addlisting' className='nav-link'>
                  Add Listing
                </Link>
              </StyledNavLink>
            )}
          </Nav>
          <Nav id='nav-right'>

          {isAuthenticated && (
                        <Link to='/profile'>
           <StyledImage src={user.picture}/>
           </Link>
            )}

             <AuthenticationButton />
          </Nav>
        </StyledCollapse>
      </Container>
    </StyledNavbar>
  );
};

export default NavigationBar;
