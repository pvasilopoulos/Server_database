-- MySQL Script generated by MySQL Workbench
-- Κυρ 11 Αυγούστου 2020 17:37:07 μμ EEST
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema SmartPark
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `SmartPark` ;
USE `SmartPark` ;

-- -----------------------------------------------------

-- Table `SmartPark`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartPark`.`user` (
  `username` VARCHAR(16) NOT NULL,
  `email` VARCHAR(255) NOT NULL,
  `password` VARCHAR(32) NOT NULL,
  `id` INT NOT NULL,
  `car_type` VARCHAR(128) NULL,
  `name` VARCHAR(64) NULL,
  `mobile` VARCHAR(64) ASCII NULL,
  `address` VARCHAR(128) NULL,
  PRIMARY KEY (`id`));


-- -----------------------------------------------------
-- Table `SmartPark`.`slot`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartPark`.`slot` (  
  `slot_id` VARCHAR(45) NOT NULL,
  `Latitude` FLOAT(10),
  `Longitude` FLOAT(10),
  `state` VARCHAR(1) NULL,
  `name` VARCHAR(45) NULL,
  PRIMARY KEY (`slot_id`));

-- -----------------------------------------------------
-- Table `SmartPark`.`Booking`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartPark`.`Booking` (
  `available_space` VARCHAR(45) NULL,
  `available_time` VARCHAR(45) NULL,
  `duration` FLOAT(4),
  `location` VARCHAR(45) NULL,
  `time` TIMESTAMP NOT NULL,
  `date` DATETIME NOT NULL,
  `user_id` INT NULL,
  `booking_id` INT NOT NULL,
  `slot_slot_id` VARCHAR(45) NOT NULL,
  `amount_ID`INT(5) NOT NULL,
  `amount` FLOAT(40) NULL,  
  INDEX `fk_Booking_user_idx` (`user_id` ASC) VISIBLE,
  PRIMARY KEY (`booking_id`),
  INDEX `fk_Booking_slot1_idx` (`slot_slot_id` ASC) VISIBLE,
  CONSTRAINT `fk_Booking_user`
    FOREIGN KEY (`user_id`)
    REFERENCES `SmartPark`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Booking_slot1`
    FOREIGN KEY (`slot_slot_id`)
    REFERENCES `SmartPark`.`slot` (`slot_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
	


-- Table `SmartPark`.`admin`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartPark`.`admin` (
  `username` VARCHAR(16) NOT NULL,
  `email` VARCHAR(255) NOT NULL,
  `password` VARCHAR(32) NOT NULL,
  `id`  NOT NULL,
  PRIMARY KEY (`id`));


-- -----------------------------------------------------
-- Table `SmartPark`.`booking_service`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartPark`.`booking_service` (
  `user_id`  NOT NULL,
  `booking_service_id` INT NOT NULL,
  `Booking_booking_id` INT NOT NULL,
  `slot_slot_id` VARCHAR(45) NOT NULL,
  INDEX `fk_booking_service_user_idx` (`user_id` ASC) VISIBLE,
  PRIMARY KEY (`booking_service_id`),
  INDEX `fk_booking_service_Booking_idx` (`Booking_booking_id` ASC) VISIBLE,
  INDEX `fk_booking_service_slot_idx` (`slot_slot_id` ASC) VISIBLE,
  CONSTRAINT `fk_booking_service_user`
    FOREIGN KEY (`user_id`)
    REFERENCES `SmartPark`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_booking_service_Booking`
    FOREIGN KEY (`Booking_booking_id`)
    REFERENCES `SmartPark`.`Booking` (`booking_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_booking_service_slot`
    FOREIGN KEY (`slot_slot_id`)
    REFERENCES `SmartPark`.`slot` (`slot_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
