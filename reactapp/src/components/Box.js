import {animated} from "react-spring";

import "./Box.css"

export const Box = ({style, item}) => {

    if (style && item) {
        style.backgroundColor = item.color;
    }

    return (
        <animated.div style={style} className="box">
            {item.color}
        </animated.div>
    );
};