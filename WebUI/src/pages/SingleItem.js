import { useState, useEffect } from 'react';
import { Container, Row, Col, Card, ListGroup, ListGroupItem, Button, Carousel, CarouselItem, ProgressBar } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { BsHeart } from 'react-icons/bs';

const serverUrl = process.env.REACT_APP_SERVER_URL;
const baseURL = `${serverUrl}/api/items`;

const SingleItem = () => {
    return (
        <Container>
            <Row className='g-4 mt-2'>
                <Col md='9'>
                    <Carousel fade>
                        <Carousel.Item>
                            <img
                                className='d-block w-100'
                                src='https://images.unsplash.com/photo-1493663284031-b7e3aefcae8e?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80'
                                alt='First slide'
                            />
                        </Carousel.Item>
                        <Carousel.Item>
                            <img
                                className='d-block w-100'
                                src='https://images.unsplash.com/photo-1493663284031-b7e3aefcae8e?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80'
                                alt='Second slide'
                            />
                        </Carousel.Item>
                        <Carousel.Item>
                            <img
                                className='d-block w-100'
                                src='https://images.unsplash.com/photo-1493663284031-b7e3aefcae8e?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80'
                                alt='Third slide'
                            />
                        </Carousel.Item>
                    </Carousel>
                    <div>
                        <span class='mt-5'>Time left to participate:</span>
                        <ProgressBar striped variant='primary' now={80} label={`3 hours`} />
                    </div>
                </Col>
                <Col md='3'>
                    <Card>
                        <Card.Body>
                            <Card.Title>Almost new sofa</Card.Title>
                            <Card.Subtitle className='mb-2 text-muted'>Furniture</Card.Subtitle>
                            <Card.Text>
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut consectetur, ipsum sed pretium porttitor, ligula
                                sapien elementum elit, at facilisis elit odio eu diam. Quisque sed velit fermentum, gravida diam ut, ornare
                                elit.
                            </Card.Text>
                            <div className='d-grid gap-2'>
                                <Button variant='primary'>
                                    <b>Apply</b>
                                </Button>
                                <Button variant='outline-secondary'>
                                    <BsHeart /> Add to favorites
                                </Button>
                            </div>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    );
};

export default SingleItem;
