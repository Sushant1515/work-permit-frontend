import * as React from "react";
import { Col, Row, Card } from "react-bootstrap";
import { Field } from "@progress/kendo-react-form";
import { Button } from "@progress/kendo-react-buttons";
import {
  FormInput,
  FormDropDownList,
  FormDatePicker,
  FormTextArea,
} from "./form-componant";

import {
  dropDownList,
  formInput,
  selectCategory,
  selectLocation,
  selectStatus,
  phoneValidator,
} from "./validators";

const jenisPenyakit = ["", "Flu", "Batuk", "Tipes"];
const kategoriPenyakit = ["", "Menular", "Tidak Menular"];
const Gendre = ["", "Male", "Female", "other"];
const lokasiPerawatan = ["", "Rumah Sakit", "Puskesmas", "Rumah"];

export const RestData = (
  <div>
    <p>job Description</p>
    <Card>
      <Card.Body>
        <Row>
          <Col xs={4} md={4}>
            <Field
              key={"Tittle"}
              id={"Tittle"}
              name={"Tittle"}
              label={"No.Register"}
              type={"text"}
              component={FormInput}
              validator={formInput}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
          <Col xs={4} md={4}>
            <Field
              key={"Valid From"}
              id={"Valid From"}
              name={"Valid From"}
              label={"Date"}
              component={FormDatePicker}
              validator={formInput}
              defaultValue=""
            />
          </Col>
          <Col xs={4} md={4}>
            <Field
              key={"Number"}
              id={"Number"}
              name={"Number"}
              label={"work order"}
              type={"text"}
              component={FormInput}
              validator={phoneValidator}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
        </Row>
        <br></br>
        <Row>
          <Col xs={4} md={4}>
            <Field
              key={"Tittle"}
              id={"Tittle"}
              name={"Tittle"}
              label={"Main Tag"}
              type={"text"}
              component={FormInput}
              validator={formInput}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
          <Col xs={4} md={4}>
            <Field
              key={"Number"}
              id={"Number"}
              name={"Number"}
              label={"Tag Number"}
              type={"text"}
              component={FormInput}
              validator={phoneValidator}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
          <Col xs={4} md={4}>
            <Field
              key={"Number"}
              id={"Number"}
              name={"Number"}
              label={"Descipline"}
              type={"text"}
              component={FormInput}
              validator={phoneValidator}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
        </Row>
        <Row>
          <Col xs={4} md={4}>
            <Field
              key={"Tittle"}
              id={"Tittle"}
              name={"Tittle"}
              label={"Job location"}
              type={"text"}
              component={FormInput}
              validator={formInput}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
          <Col xs={4} md={4}>
            <Field
              key={"Number"}
              id={"Number"}
              name={"Number"}
              label={"System Name"}
              type={"text"}
              component={FormInput}
              validator={phoneValidator}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
          <Col xs={4} md={4}>
            <Field
              key={"Number"}
              id={"Number"}
              name={"Number"}
              label={"Job Tittle"}
              type={"text"}
              component={FormInput}
              validator={phoneValidator}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
        </Row>
        <Row>
          <Col xs={12} md={6}>
            <Field
              key={"Valid From"}
              id={"Valid From"}
              name={"Valid From"}
              label={"From Date"}
              component={FormDatePicker}
              validator={formInput}
              defaultValue=""
            />
          </Col>
          <Col xs={12} md={6}>
            <Field
              key={"Valid From"}
              id={"Valid From"}
              name={"Valid From"}
              label={"To Date"}
              component={FormDatePicker}
              validator={formInput}
              defaultValue=""
            />
          </Col>
        </Row>
        <Row>
          <Col xs={12} md={12}>
            <Field
              key={"Description"}
              id={"Description"}
              name={"Description"}
              label={" job Description"}
              type={"Description"}
              component={FormTextArea}
              validator={formInput}
              defaultValue={""}
              as={"TextArea"}
              placeHolder={"Text Area"}
            />
          </Col>
        </Row>
      </Card.Body>
    </Card>
  </div>
);
