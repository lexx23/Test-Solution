import React from 'react';
import {useTransition} from 'react-spring'

import {useAddBoxMutation, useDeleteBoxMutation, useGetBoxesQuery} from "../services/boxes";

import './Grid.css'
import {Box} from "./Box";

const colors = ["blue", "black", "green", "red"];


export function Grid() {

    const [addBox] = useAddBoxMutation();
    const [deleteBox] = useDeleteBoxMutation()
    const {data: boxes} = useGetBoxesQuery();

    const onAdd = async () => {

        try {

            const colorIndex = Math.floor(Math.random() * 3)
            const color = colors[colorIndex];
            const data = {color: color};

            await addBox(data);

        } catch {
            console.error("Error on box adding");
        }
    }

    const onDelete = async () => {

        if (boxes.length === 0 || !boxes) {
            return;
        }

        try {

            const item = boxes[boxes.length - 1];
            await deleteBox(item.id);

        } catch {
            console.error("Error on box deleting");
        }
    };

    const transBoxes = useTransition(
        boxes ? boxes.map(data => ({...data})) : [],
        {
            key: (item) => item.id,
            from: {opacity: 0, maxWidth: "0%"},
            enter: {opacity: 1, maxWidth: "20%", transform: "translateX(0px)"},
            leave: {opacity: 0, transform: "translateX(200px)"},
            config: {
                duration: 800
            }
        });

    return (
        <div>
            <button onClick={onAdd}>Add</button>
            <button onClick={onDelete}>Delete</button>
            <div className="boxes">
                {transBoxes((style, item) =>
                        item && (
                            <Box key={item.id} style={style} item={item}/>
                        )
                )}
            </div>
        </div>
    );
}
