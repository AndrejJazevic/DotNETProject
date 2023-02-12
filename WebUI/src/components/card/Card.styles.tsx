import styled from 'styled-components';

export const ImageDiv = styled.div`
    height: 0;
    padding-bottom: 65%;
    overflow: hidden;
    position: relative;
    border-radius: 4px;
    background-color: #aeaeae;
`;

export const InformationDiv = styled.div`
    display: flex;
    justify-content: space-between;
    margin-top: 8px;
`;

export const Image = styled.img`
    width: 100%;
    position: absolute;
    top: 0;
    bottom: 0;
    margin: auto;
`;

export const Information = styled.div`
    // this will need fixing in a later stage
    max-width: 80%;
`;

export const Title = styled.div`
    font-family: 'Raleway', sans-serif;
    font-size: 15px;
    font-weight: 700;
`;

export const Uploader = styled.div`
    font-family: 'Roboto', sans-serif;
    font-size: 12px;
`;

export const Stats = styled.div`
    margin-top: 4px;
`;
