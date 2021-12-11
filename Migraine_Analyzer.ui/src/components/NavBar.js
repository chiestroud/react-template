import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  Nav,
  NavItem,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  NavbarText,
  Button
} from 'reactstrap';
import { signInUser, signOutUser } from '../helpers/auth';

const NavBar = () => {
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <div>
      <Navbar color="light" light expand="md">
        <Link className='navbar-brand' to="/">Home</Link>
        <NavbarToggler onClick={toggle} />
        <Collapse isOpen={isOpen} navbar>
          <Nav className="mr-auto" navbar>
            <NavItem>
              <Link className='nav-link' to="/first">First Page</Link>
            </NavItem>
            <NavItem>
              <Link className='nav-link' to="/second">Second Page</Link>
            </NavItem>
            <UncontrolledDropdown nav inNavbar>
              <DropdownToggle nav caret>
                Dropdown
              </DropdownToggle>
              <DropdownMenu right>
                <DropdownItem>
                  <Link className='nav-link' to="/drop-down1">Dropdown Link1</Link>
                </DropdownItem>
                <DropdownItem>
                  <Link className='nav-link' to="/drop-down2">Dropdown Link2</Link>
                </DropdownItem>
              </DropdownMenu>
            </UncontrolledDropdown>
            <Button color='info' onClick={signInUser}>Sign In</Button>
            <Button color='danger' onClick={signOutUser}>Log Out</Button>
          </Nav>
          <NavbarText>Welcome</NavbarText>
        </Collapse>
      </Navbar>
    </div>
  );
};

export default NavBar;
