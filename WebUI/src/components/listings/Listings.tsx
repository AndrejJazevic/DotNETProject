import { Col } from 'react-bootstrap';
import styled from 'styled-components';
import Card from '../card/Card';
import CardSkeleton from '../card/CardSkeleton';

interface Item {
    name: string;
    uploader: string;
    imageUrls: string[];
}

interface Props {
    list: Item[];
}

const StyledCol = styled(Col)`
    margin-bottom: 16px;
`;

const Listings = ({ list }: Props) => {
    const LoadSkeletons: Function = () => {
        let skeletonList = [];

        for (let i = 1; i <= 8; i++) {
            skeletonList.push(
                <StyledCol md={6} lg={4} xl={3}>
                    <CardSkeleton />
                </StyledCol>
            );
        }

        return skeletonList;
    };

    const LoadListings: Function = () => {
        return list.map(item => (
            <StyledCol md={6} lg={4} xl={3}>
                <Card title={item.name || undefined} uploader={item.uploader || undefined} image={item.imageUrls[0]} />
            </StyledCol>
        ));
    };

    return list == null ? <LoadSkeletons /> : <LoadListings />;
};

export default Listings;
