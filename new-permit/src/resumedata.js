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

export const ResumeData = (
  <div>
    <Card>
      <Card.Body>
        <Row>
          <Col xs={6} md={6}>
            <Field
              key={"Tittle"}
              id={"Tittle"}
              name={"Tittle"}
              label={"Tittle"}
              type={"text"}
              component={FormInput}
              validator={formInput}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
          <Col xs={6} md={6}>
            <Field
              key={"Number"}
              id={"Number"}
              name={"Number"}
              label={"Number"}
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
          <Col xs={12} md={12}>
            <Field
              key={"Description"}
              id={"Description"}
              name={"Description"}
              label={"Description"}
              type={"Description"}
              component={FormTextArea}
              validator={formInput}
              defaultValue={""}
              as={"TextArea"}
              placeHolder={"Text Area"}
            />
          </Col>
        </Row>
        <br></br>
        <Row>
          <Col xs={12} md={6}>
            <Field
              key={"Asset"}
              id={"Asset"}
              name={"Asset"}
              label={"Asset"}
              component={FormDropDownList}
              validator={selectCategory}
              data={kategoriPenyakit}
              as={"select"}
              defaultValue=""
              placeHolder={"Asset1"}
            />
          </Col>
          <br></br>
          <Col xs={12} md={6}>
            <Field
              key={"Type"}
              id={"Type"}
              name={"Type"}
              label={"Type"}
              component={FormDropDownList}
              validator={selectStatus}
              data={Gendre}
              as={"select"}
              defaultValue=""
              placeHolder={"Type1"}
            />
          </Col>
        </Row>
        <br></br>
        <Row>
          <Col xs={12} md={6}>
            <Field
              key={"Area"}
              id={"Area"}
              name={"Area"}
              label={"Area"}
              component={FormDropDownList}
              validator={selectCategory}
              data={kategoriPenyakit}
              as={"select"}
              defaultValue=""
              placeHolder={"Area1"}
            />
          </Col>
          <br></br>
          <Col xs={12} md={6}>
            <Field
              key={"Sub Type"}
              id={"Sub Type"}
              name={"Sub Type"}
              label={"Sub Type"}
              component={FormDropDownList}
              validator={selectStatus}
              data={Gendre}
              as={"select"}
              defaultValue=""
              placeHolder={"sub Type1"}
            />
          </Col>
        </Row>
        <br></br>
        <Row>
          <Col xs={12} md={6}>
            <Field
              key={"Zone"}
              id={"Zone"}
              name={"Zone"}
              label={"Zone"}
              type={"text"}
              component={FormInput}
              validator={formInput}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
          <Col xs={12} md={6}>
            <Field
              key={"Valid From"}
              id={"Valid From"}
              name={"Valid From"}
              label={"Valid From"}
              component={FormDatePicker}
              validator={formInput}
              defaultValue=""
            />
          </Col>
        </Row>
        <Row>
          <Col xs={12} md={6}>
            <Field
              key={"System"}
              id={"System"}
              name={"System"}
              label={"System"}
              type={"text"}
              component={FormInput}
              validator={formInput}
              defaultValue={""}
              placeHolder={"Filled Text"}
            />
          </Col>
          <Col xs={12} md={6}>
            <Field
              key={"Valid To"}
              id={"Valid To"}
              name={"Valid To"}
              label={"Valid To"}
              component={FormDatePicker}
              validator={formInput}
              defaultValue=""
            />
          </Col>
        </Row>

        <Row>
          <Col xs={12} md={6}>
            <Field
              key={"Disciplin"}
              id={"Disciplin"}
              name={"Disciplin"}
              label={"Disciplin"}
              type={"text"}
              component={FormInput}
              validator={formInput}
              placeHolder={"Filled Text"}
            />
          </Col>
          <Col xs={12} md={6}>
            <Field
              key={"Applicant"}
              id={"Applicant"}
              name={"Applicant"}
              label={"Applicant"}
              component={FormDropDownList}
              data={lokasiPerawatan}
              as={"select"}
              defaultValue=""
            />
          </Col>
        </Row>
        <Row>
          <Col xs={12} md={6}>
            <Field
              key={"Safe job analysis"}
              id={"Safe job analysis"}
              name={"Safe job analysis"}
              label={"Safe job Analysis"}
              type={"text"}
              component={FormInput}
              validator={formInput}
              placeHolder={"Filled Text"}
            />
          </Col>
          <Col xs={12} md={6}>
            <Field
              key={"Work order"}
              id={"Work order"}
              name={"Work order"}
              label={"Work order"}
              component={FormDropDownList}
              validator={dropDownList}
              data={jenisPenyakit}
              as={"select"}
              defaultValue=""
            />
          </Col>
        </Row>
      </Card.Body>
    </Card>
  </div>
);
