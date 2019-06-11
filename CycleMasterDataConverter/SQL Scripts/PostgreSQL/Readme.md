### Preface:

After much experimentation, I realized to import the majority of this CycleMaster data into Garmin Connect required using Garmin's TCX XML file format.  It would still require manually editing each ride in Garmin Connect, but I would need to do this anyway to modify the title, notes, and assign the proper gear.  My only option to achieve this was to import the CSV files into my PostgreSQL database and use the XML functions to generate the TCX files.

**Instructions:**  
After generation of the CSV files, use these seven SQL scripts to build and export the TCX files.  Be forewarned, depending on how much ride data you have, THESE SCRIPTS CAN TAKE A LONG TIME!  Case in point, the fourth and fifth SQL scripts took over an hour to complete on my test server.


**Steps**:

1. "01_create_tables.sql" - As the name suggests, this script creates the tables needed for importing the CSV files, within a "cyclemaster" schema.  Run this script against a pre-existing database of your choosing, ensuring that the user has schema creation rights.

2. "02_import_csv.sql" - This script imports the CSV files into the newly created tables.  It must be run from a terminal in the same directory as the CSV files, using psql.

3. "03_build_tcx_track_points.sql" - Scripts #3 through #7 can be run from a tool like pgAdmin.  Prepare to wait with this script.  It builds the "Trackpoint" piece for every coordinate in the "ridedata" table.

4. "04_build_tcx_tracks.sql" - Another script that will take a long time!  This one builds the tracks for each and every ride.  Side note, this is where the "point_id" field comes into play.

5. "05_finalize_tcx_tracks.sql" - This script completes building the TCX file for each ride, storing the results into the "ridemetadata" table.

6. "06_generate_tcx_export_lines.sql" - Ok, so the TCX files have been generated and are stored in PostgreSQL, now what?  We can get them out using PostgreSQL's "\copy" command, yet we need one line per TCX file we want to export.  If you have many rides, creating these lines manually is not pratical.  So, this script does this for us from the "ridemetadata" table.  I suggest running this script in pgAdmin (or similar), copy the results to a text editor, and then save to a SQL script with a name of your choosing.

7. "[generated script]" - Create a folder to house the TCX files, and move the generated script from the previous step to this folder.  Now, in this folder, run the generated script from a terminal using psql.

8. After the TCX files are extracted, an adjustment needs to be made to each file.  PostgreSQL surrounds the XML in the TCX with double quotes, and these need to be removed, otherwise Garmin Connect will report errors.  Removing these quotes is easy to do with a tool like Notepad++.

9. "07_export_metadata.sql" - To help with the manual editing of your rides in Garmin Connect, this script exports certain values from the "ridemetadata" table.  Adjust the timezone as needed.
