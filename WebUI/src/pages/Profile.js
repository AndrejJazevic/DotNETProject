import React from 'react';

import { useAuth0, withAuthenticationRequired } from '@auth0/auth0-react';
import { Col, Container, Row } from 'react-bootstrap';
import Loading from '../components/loading';

const Profile = () => {
  const { user } = useAuth0();
  const { picture, email, nickname } = user;
  return (
    <Container>
      <Row>
        <div class='card p-4'>
          <div class=' image d-flex flex-column justify-content-center align-items-center'>
            {' '}
            <button class='btn btn-secondary'>
              {' '}
              <img src={picture} height='100' width='100'/>
            </button>{' '}
            <span class='name mt-3'>{email}</span> <span class='idd'>@{user.sub}</span>
            <div class='d-flex flex-row justify-content-center align-items-center gap-2'>
              {' '}
              <span>
                <i class='fa fa-copy'></i>
              </span>{' '}
            </div>
            <div class='d-flex flex-row justify-content-center align-items-center mt-3'>
              {' '}
              <span class='number'>
                1069 <span class='follow'>Total listings</span>
              </span>{' '}
            </div>
            <div class=' d-flex mt-2'>
              {' '}
              <button class='btn1 btn-dark'>Edit Profile</button>{' '}
            </div>
            <div class='text mt-3'>
              {' '}
            </div>
            <div class='gap-3 mt-3 icons d-flex flex-row justify-content-center align-items-center'>
              {' '}
              <span>
                <i class='fa fa-twitter'></i>
              </span>{' '}
              <span>
                <i class='fa fa-facebook-f'></i>
              </span>{' '}
              <span>
                <i class='fa fa-instagram'></i>
              </span>{' '}
              <span>
                <i class='fa fa-linkedin'></i>
              </span>{' '}
            </div>
            <div class=' px-2 rounded mt-4 date '>
              {' '}
              <span class='join'>Joined {user.updated_at}</span>{' '}
            </div>
          </div>
        </div>
      </Row>
    </Container>
  );
};

export default Profile;
