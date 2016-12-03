import React from 'react';
import renderer from 'react-test-renderer';
import PostList from '../Scripts/PostList.jsx';

describe('PostList (Snapshot)', () => {
    it('PostList renders PostList', () => {
        const someData = [];
        const component = renderer.create(<PostList data={someData}/>);
        const json = component.toJSON();
        expect(json).toMatchSnapshot();
    });
});
