-- --------------------------------------------------------
-- Host:                         10.121.41.104
-- Server version:               10.3.11-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Dumping structure for table ami_lt.product
DROP TABLE IF EXISTS `product`;
CREATE TABLE IF NOT EXISTS `product` (
  `idproduct` int(11) NOT NULL AUTO_INCREMENT,
  `pid` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `panelid` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `cosmetic_emno` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `display_emno` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `cp_aoi_judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `cosmetic_top_judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `cosmetic_bottom_judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `function_judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `display_judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `pre_align_judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `display_align_judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `pchk_judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `eicr_judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `apd_judge` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `priority_defect_name` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `priority_reason_code` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `priority_defect_name_cosmetic_top` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `priority_reason_code_cosmetic_top` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `priority_defect_name_cosmetic_bottom` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `priority_reason_code_cosmetic_bottom` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `priority_defect_name_function` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `priority_reason_code_function` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `priority_defect_name_display` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `priority_reason_code_display` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `top_priority_insp_type` tinyint(4) DEFAULT -1,
  `vision_cosmetic_top_total` int(11) NOT NULL DEFAULT -1,
  `vision_cosmetic_bottom_total` int(11) NOT NULL DEFAULT -1,
  `vision_display_total` int(11) NOT NULL DEFAULT -1,
  `vision_cosmetic_top_ng` int(11) DEFAULT -1,
  `vision_cosmetic_bottom_ng` int(11) DEFAULT -1,
  `vision_display_ng` int(11) DEFAULT -1,
  `pre_align_calibration_x` double(22,6) DEFAULT 0.000000,
  `pre_align_calibration_y` double(22,6) DEFAULT 0.000000,
  `pre_align_calibration_theta` double(22,6) DEFAULT 0.000000,
  `display_align_calibration_x` double(22,6) DEFAULT 0.000000,
  `display_align_calibration_y` double(22,6) DEFAULT 0.000000,
  `stage_index` int(11) DEFAULT -1,
  `pre_align_index` int(11) DEFAULT -1,
  `display_align_index` int(11) DEFAULT -1,
  `cosmetic_insp` tinyint(4) DEFAULT -1,
  `display_insp` tinyint(4) DEFAULT -1,
  `user_id` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `model_id` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `pchk_model_name` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `mes_online` tinyint(4) DEFAULT -1,
  `start_datetime` datetime DEFAULT NULL,
  `end_datetime` datetime DEFAULT NULL,
  `start_datetime_pre_align` datetime DEFAULT NULL,
  `end_datetime_pre_align` datetime DEFAULT NULL,
  `start_datetime_cosmetic_top` datetime DEFAULT NULL,
  `end_datetime_cosmetic_top` datetime DEFAULT NULL,
  `start_datetime_cosmetic_bottom` datetime DEFAULT NULL,
  `end_datetime_cosmetic_bottom` datetime DEFAULT NULL,
  `start_datetime_display_align` datetime DEFAULT NULL,
  `end_datetime_display_align` datetime DEFAULT NULL,
  `start_datetime_display` datetime DEFAULT NULL,
  `end_datetime_display` datetime DEFAULT NULL,
  `start_datetime_display_unloading` datetime DEFAULT NULL,
  `end_datetime_display_unloading` datetime DEFAULT NULL,
  `pre_align_tacttime` double(22,2) DEFAULT 0.00,
  `cosmetic_top_tacttime` double(22,2) DEFAULT 0.00,
  `cosmetic_bottom_tacttime` double(22,2) DEFAULT 0.00,
  `display_align_tacttime` double(22,2) DEFAULT 0.00,
  `display_tacttime` double(22,2) DEFAULT 0.00,
  `display_unloading_tacttime` double(22,2) DEFAULT 0.00,
  `output_logic` int(11) DEFAULT -1,
  `plc_forceport` int(11) DEFAULT -1,
  `scrap_zone` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT 'N/A',
  `pt_datetime` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`idproduct`,`pt_datetime`),
  KEY `INDEX_PID` (`pid`),
  KEY `INDEX_PANELID` (`panelid`),
  KEY `INDEX_STARTDATETIME` (`start_datetime`),
  KEY `INDEX_ENDDATETIME` (`end_datetime`)
) ENGINE=InnoDB AUTO_INCREMENT=3668765 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
 PARTITION BY RANGE (to_days(`pt_datetime`))
(PARTITION `p2023_1` VALUES LESS THAN (738917) ENGINE = InnoDB,
 PARTITION `p2023_2` VALUES LESS THAN (738945) ENGINE = InnoDB,
 PARTITION `p2023_3` VALUES LESS THAN (738976) ENGINE = InnoDB,
 PARTITION `p2023_4` VALUES LESS THAN (739006) ENGINE = InnoDB,
 PARTITION `p2023_5` VALUES LESS THAN (739037) ENGINE = InnoDB,
 PARTITION `p2023_6` VALUES LESS THAN (739067) ENGINE = InnoDB,
 PARTITION `p2023_7` VALUES LESS THAN (739098) ENGINE = InnoDB,
 PARTITION `p2023_8` VALUES LESS THAN (739129) ENGINE = InnoDB,
 PARTITION `p2023_9` VALUES LESS THAN (739159) ENGINE = InnoDB,
 PARTITION `p2023_10` VALUES LESS THAN (739190) ENGINE = InnoDB,
 PARTITION `p2023_11` VALUES LESS THAN (739220) ENGINE = InnoDB,
 PARTITION `p2023_12` VALUES LESS THAN (739251) ENGINE = InnoDB,
 PARTITION `pMax` VALUES LESS THAN MAXVALUE ENGINE = InnoDB);

-- Data exporting was unselected.

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
