import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';


const HeaderComponent = () => (
    <AppBar position="static">
        <Toolbar>
            <Typography variant="h6">
                Music engine
            </Typography>
        </Toolbar>
    </AppBar>
);


export default HeaderComponent;
