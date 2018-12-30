SELECT '\copy (SELECT tcx_final FROM cyclemaster.ridemetadata WHERE ride_id=''' || ride_id || ''') TO ''' || ride_id || '.tcx'' QUOTE ''|'' CSV;'
FROM cyclemaster.ridemetadata;
