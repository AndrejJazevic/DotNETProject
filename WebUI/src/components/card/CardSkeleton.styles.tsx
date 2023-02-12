import { Placeholder } from 'react-bootstrap';
import styled from 'styled-components';

export const Skeleton = styled(Placeholder)`
    min-height: 0;
    border-radius: 4px;
    cursor: default;
`;

export const ImageDiv = styled.div`
    height: 0;
    padding-bottom: 65%;
    overflow: hidden;
    position: relative;
    border-radius: 4px;
    background-color: #aeaeae;
`;

export const Image = styled(Skeleton)`
    height: 0;
    width: 100%;
    padding-bottom: 65%;
`;

export const InformationDiv = styled.div`
    display: flex;
    justify-content: space-between;
    margin-top: 8px;
`;

export const Title = styled(Skeleton)`
    height: 15px;
    width: 150px;
    display: block;
`;

export const Uploader = styled(Skeleton)`
    height: 12px;
    width: 75px;
`;

export const Icon = styled(Skeleton)`
    height: 16px;
    width: 16px;
`;
